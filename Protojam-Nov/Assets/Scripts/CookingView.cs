using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

public class CookingView : MonoBehaviour
{
    [Header("Resources")]
    public Sprite[] zombieSprites;
    public Sprite[] materialSprites;

    #region Order

    [Header("Order")] 
    [SerializeField] private Image zombieImage;
    [SerializeField] private GameObject orderPanel;
    [SerializeField] private CombinedMaterialView orderMaterialView;
    [SerializeField] private CombinedMaterialView submittedMaterialView;
    
    [SerializeField] private GameObject orderCorrect;
    [SerializeField] private GameObject orderWrong;

    public void ShowOrderInformation(Order order)
    {
        zombieImage.sprite = zombieSprites[(int)order.ZombieType];
        zombieImage.color = Color.white;

        orderPanel.SetActive(true);
        orderMaterialView.Set(order.Recipe);
    }
    
    public void ShowSubmitResult(bool isCorrect, bool leave, Action onCompleted)
    {
        submittedMaterialView.gameObject.SetActive(true);
        submittedMaterialView.Set(_combinedMaterials.ToArray());
        StartCoroutine(CoShowSubmitResult(isCorrect, leave, onCompleted));
    }

    private IEnumerator CoShowSubmitResult(bool isCorrect, bool leave, Action onCompleted)
    {
        Stage.Instance.isTimerPlaying = false;
        orderCorrect.SetActive(isCorrect);
        orderWrong.SetActive(!isCorrect);
        orderPanel.SetActive(false);

        yield return new WaitForSeconds(1f);

        orderCorrect.SetActive(false);
        orderWrong.SetActive(false);
        submittedMaterialView.gameObject.SetActive(false);
        submittedMaterialView.Set(Array.Empty<MaterialType>());

        orderPanel.SetActive(!leave);
        if (leave)
        {
            zombieImage.color = Color.clear;
        }

        yield return new WaitForSeconds(0.5f);
        Stage.Instance.isTimerPlaying = true;
        ClearCombined();
        onCompleted?.Invoke();
    }

    #endregion

    #region Combination

    [Header("Combination")]
    [SerializeField] private CombinedMaterialView combinedMaterialView;

    private readonly List<MaterialType> _combinedMaterials = new();

    private void ClearCombined()
    {
        _combinedMaterials.Clear();
        combinedMaterialView.Set(_combinedMaterials.ToArray());
    }

    private void AddCombined(MaterialType materialType)
    {
        if (_combinedMaterials.Count >= Order.MaterialCount) return;
        if (!GameManager.Instance.IsGamePlaying) return;

        _combinedMaterials.Add(materialType);
        combinedMaterialView.Set(_combinedMaterials.ToArray());
    }

    #endregion

    #region Drag

    [Header("Dragable")] 
    [SerializeField] private Transform[] materialSlotsTransform;
    [SerializeField] private Image[] materialSlotsImages;

    [Header("Drag Targets")]
    [SerializeField] private RectTransform submitTransform;
    [SerializeField] private RectTransform combineBarTransform;
    [SerializeField] private RectTransform trashTransform;
    
    private readonly List<IDisposable> _playingDisposable = new List<IDisposable>();

    private void InitMaterialSlotsDrag()
    {
        for (var i = 0; i < materialSlotsTransform.Length; i++)
        {
            var materialSlotImage = materialSlotsImages[i];
            var materialSlot = materialSlotsTransform[i];            
            var materialType = (MaterialType)i;

            var startPosition = Vector2.zero;
            var targetBegin = Vector2.zero;
            var dragBegin = Vector2.zero;
            Vector2 moveDelta;
            var headerEventTrigger = materialSlot.gameObject.AddComponent<ObservableEventTrigger>();
            
            headerEventTrigger.OnPointerDownAsObservable().Subscribe(eventData =>
            {
                startPosition = materialSlot.position;
                targetBegin = startPosition;
                dragBegin = eventData.position;

                materialSlotImage.color = Color.white;
            }).AddTo(_playingDisposable);
            headerEventTrigger.OnDragAsObservable().Subscribe(eventData =>
            {
                moveDelta = eventData.position - dragBegin;
                materialSlot.position = targetBegin + moveDelta;
            }).AddTo(_playingDisposable);
            headerEventTrigger.OnPointerUpAsObservable().Subscribe(eventData =>
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(combineBarTransform, eventData.position))
                {
                    AddCombined(materialType);
                }

                materialSlotImage.color = Color.clear;
                materialSlot.position = startPosition;
            }).AddTo(_playingDisposable);
        }
    }

    private void InitCombinedDrag()
    {
        var combined = combinedMaterialView.transform;
        var startPosition = Vector2.zero;
        var targetBegin = Vector2.zero;
        var dragBegin = Vector2.zero;
        Vector2 moveDelta;
        var headerEventTrigger = combined.gameObject.AddComponent<ObservableEventTrigger>();
            
        headerEventTrigger.OnPointerDownAsObservable().Subscribe(eventData =>
        {
            startPosition = combined.position;
            targetBegin = startPosition;
            dragBegin = eventData.position;
        }).AddTo(_playingDisposable);
        headerEventTrigger.OnDragAsObservable().Subscribe(eventData =>
        {
            moveDelta = eventData.position - dragBegin;
            combined.position = targetBegin + moveDelta;
        }).AddTo(_playingDisposable);
        headerEventTrigger.OnPointerUpAsObservable().Subscribe(eventData =>
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(submitTransform, eventData.position))
            {
                Stage.Instance.SubmitOrder(_combinedMaterials.ToArray());
                ClearCombined();
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(trashTransform, eventData.position))
            {
                ClearCombined();
            }
            combined.position = startPosition;
        }).AddTo(_playingDisposable);
    }
    
    #endregion
    
    private void Awake()
    {
        InitMaterialSlotsDrag();
        InitCombinedDrag();
    }

    public void OnEndGame()
    {
        while (_playingDisposable.Count > 0)
        {
            _playingDisposable[0].Dispose();
            _playingDisposable.RemoveAt(0);
        }

        ClearCombined();
    }
}

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
    [SerializeField] private Sprite[] zombieSprites;
    [SerializeField] private Sprite[] materialSprites;

    #region Order

    [Header("Order")] 
    [SerializeField] private Image zombieImage;
    [SerializeField] private GameObject orderRecipeImagesContainer;
    [SerializeField] private Image[] orderRecipeImages;
    
    [SerializeField] private GameObject orderCorrect;
    [SerializeField] private GameObject orderWrong;

    public void ShowOrderInformation(Order order)
    {
        zombieImage.sprite = zombieSprites[(int)order.ZombieType];
        zombieImage.color = Color.white;

        orderRecipeImagesContainer.SetActive(true);
        for (int i = 0; i < order.Recipe.Length; i++)
        {
            orderRecipeImages[i].sprite = materialSprites[(int)order.Recipe[i]];
        }
    }
    
    public void ShowSubmitResult(bool isCorrect, bool leave, Action onCompleted)
    {
        StartCoroutine(CoShowSubmitResult(isCorrect, leave, onCompleted));
    }

    private IEnumerator CoShowSubmitResult(bool isCorrect, bool leave, Action onCompleted)
    {
        orderCorrect.SetActive(isCorrect);
        orderWrong.SetActive(!isCorrect);

        yield return new WaitForSeconds(2f);

        orderCorrect.SetActive(false);
        orderWrong.SetActive(false);

        if (leave)
        {
            zombieImage.color = Color.clear;
            orderRecipeImagesContainer.SetActive(false);
        }

        yield return new WaitForSeconds(0.5f);
        onCompleted?.Invoke();
    }

    #endregion

    #region Combination

    [Header("Combination")]
    [SerializeField] private Image[] combinedMaterialImages;

    private readonly List<MaterialType> _combinedMaterials = new();

    private void ClearCombined()
    {
        _combinedMaterials.Clear();
        foreach (var image in combinedMaterialImages)
        {
            image.color = Color.clear;
        }
    }

    private void AddCombined(MaterialType materialType)
    {
        if (_combinedMaterials.Count >= combinedMaterialImages.Length) return;

        _combinedMaterials.Add(materialType);
        combinedMaterialImages[_combinedMaterials.Count - 1].sprite = materialSprites[(int)materialType];
        combinedMaterialImages[_combinedMaterials.Count - 1].color = Color.white;
    }

    #endregion

    #region Drag

    [Header("Dragable")] 
    [SerializeField] private Transform[] materialSlotsTransform;
    [SerializeField] private Image[] materialSlotsImages;
    [SerializeField] private Transform combined;

    [Header("Drag Targets")]
    [SerializeField] private RectTransform dishTransform;
    [SerializeField] private RectTransform combineBarTransform;
    [SerializeField] private RectTransform trashTransform;

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
            }).AddTo(gameObject);
            headerEventTrigger.OnDragAsObservable().Subscribe(eventData =>
            {
                moveDelta = eventData.position - dragBegin;
                materialSlot.position = targetBegin + moveDelta;
            }).AddTo(gameObject);
            headerEventTrigger.OnPointerUpAsObservable().Subscribe(eventData =>
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(combineBarTransform, eventData.position))
                {
                    AddCombined(materialType);
                }

                materialSlotImage.color = Color.clear;
                materialSlot.position = startPosition;
            }).AddTo(gameObject);
        }
    }

    private void InitCombinedDrag()
    {
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
        }).AddTo(gameObject);
        headerEventTrigger.OnDragAsObservable().Subscribe(eventData =>
        {
            moveDelta = eventData.position - dragBegin;
            combined.position = targetBegin + moveDelta;
        }).AddTo(gameObject);
        headerEventTrigger.OnPointerUpAsObservable().Subscribe(eventData =>
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(dishTransform, eventData.position))
            {
                Stage.Instance.SubmitOrder(_combinedMaterials.ToArray());
                ClearCombined();
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(trashTransform, eventData.position))
            {
                ClearCombined();
            }
            combined.position = startPosition;
        }).AddTo(gameObject);
    }
    
    #endregion
    
    private void Awake()
    {
        InitMaterialSlotsDrag();
        InitCombinedDrag();
    }
}

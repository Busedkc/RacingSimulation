using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackCarousel : MonoBehaviour, ICarousel
{
    [SerializeField] List<GameObject> tracks = null;
    [SerializeField] Button leftButton = null;
    [SerializeField] Button rightButton = null;
    [SerializeField] Transform pivot = null;
    [SerializeField] float carouselRotateSpeed;
    int currentActiveIndex;
    bool rotateLeft;
    bool rotateRight;
    Quaternion leftRotationEnd;
    Quaternion rightRotationEnd;

    private void Start()
    {
        leftButton.onClick.AddListener(ShowPreviousItem);
        rightButton.onClick.AddListener(ShowNextItem);
    }

    private void OnDestroy()
    {
        leftButton.onClick.RemoveListener(ShowPreviousItem);
        rightButton.onClick.RemoveListener(ShowNextItem);
    }

    public int GetCurrentItemIndex()
    {
        return currentActiveIndex;
    }

    public void ShowNextItem()
    {
        if(rotateLeft || rotateRight)
        {
            return;
        }
        Events.LeftOrRightButtonClicked?.Invoke();
        currentActiveIndex = currentActiveIndex < tracks.Count - 1 ? currentActiveIndex + 1 : 0;
        rightRotationEnd = pivot.transform.rotation * Quaternion.Euler(0, -120, 0);
        rotateRight = true;

    }

    public void ShowPreviousItem()
    {
        if(rotateLeft || rotateRight)
        {
            return;
        }
        Events.LeftOrRightButtonClicked?.Invoke();
        currentActiveIndex = currentActiveIndex > 0 ? currentActiveIndex - 1 : tracks.Count - 1;
        leftRotationEnd = pivot.transform.rotation * Quaternion.Euler(0, 120, 0);
        rotateLeft = true;
    }

    private void Update()
    {
        if(rotateLeft)
        {
            pivot.transform.rotation = Quaternion.Lerp(pivot.transform.rotation, leftRotationEnd,
                                                            carouselRotateSpeed * Time.deltaTime);
        }
        if(rotateRight)
        {
            pivot.transform.rotation = Quaternion.Lerp(pivot.transform.rotation, rightRotationEnd,
                                                            carouselRotateSpeed * Time.deltaTime);
        }
        if(rotateLeft && pivot.transform.rotation == leftRotationEnd)
        {
            rotateLeft = false;
        }
        if (rotateRight && pivot.transform.rotation == rightRotationEnd)
        {
            rotateRight = false;
        }
    }
}

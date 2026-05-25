using UnityEngine;
using UnityEngine.EventSystems;

public class SkillDescBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
   [SerializeField] GameObject DescBoxPopUp;

   public void OnPointerEnter(PointerEventData eventData)
    {
        DescBoxPopUp.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DescBoxPopUp.SetActive(false);
    }
}

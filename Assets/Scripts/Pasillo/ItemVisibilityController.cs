using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemVisibilityController : MonoBehaviour
{
    [System.Serializable]
    public class ItemVisibility
    {
        public GameObject item; // El objeto que se mostrará u ocultará
        public float delay; // Segundos que tardará en mostrarse/ocultarse
    }

    public List<ItemVisibility> itemsToShow; // Lista de objetos que se mostrarán
    public List<ItemVisibility> itemsToHide; // Lista de objetos que se ocultarán

    private void Start()
    {
        // Iniciar las corrutinas para mostrar y ocultar objetos
        foreach (var item in itemsToShow)
        {
            StartCoroutine(ShowItemWithDelay(item));
        }

        foreach (var item in itemsToHide)
        {
            StartCoroutine(HideItemWithDelay(item));
        }
    }

    private IEnumerator ShowItemWithDelay(ItemVisibility item)
    {
        yield return new WaitForSeconds(item.delay);
        if (item.item != null)
        {
            item.item.SetActive(true); // Mostrar el objeto
        }
    }

    private IEnumerator HideItemWithDelay(ItemVisibility item)
    {
        yield return new WaitForSeconds(item.delay);
        if (item.item != null)
        {
            item.item.SetActive(false); // Ocultar el objeto
        }
    }
}

using UnityEngine;

namespace ZFramework.Tools
{
    public class BasePanel : MonoBehaviour
    {
        protected bool isRemove = false;
        protected string panelName;

        public virtual void OpenPanel(string inputName)
        {
            panelName = inputName;
            gameObject.SetActive(true);
        }

        public virtual void ClosePanel()
        {
            isRemove = true;
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
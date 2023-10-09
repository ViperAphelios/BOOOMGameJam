using UnityEngine;

namespace Player.Arrow
{
    public class ArrowControl : MonoBehaviour
    {
        public PlayerModel ownerModel;

        public Vector2 forwardDir;

        public float flySpeed;

        // Start is called before the first frame update
        void Start()
        { }

        // Update is called once per frame
        void Update()
        { }

        /// <summary>
        ///  修正当前的箭矢的朝向
        /// </summary>
        public void CorrectArrowForward()
        {
            if (forwardDir.x < 0)
            {
                transform.localScale = new Vector3(-1f, 1, 1);
            }

            if (forwardDir.x > 0)
            {
                transform.localScale = new Vector3(1f, 1, 1);
            }
        }
    }
}
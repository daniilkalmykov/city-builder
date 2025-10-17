    using UnityEngine;
    using UnityEngine.UIElements;

    namespace Presentation.Views
    {
        [RequireComponent(typeof(UIDocument))]
        internal abstract class LayoutViewBase : MonoBehaviour
        {
            protected VisualElement root;
            protected UIDocument uiDocument;
    
            public virtual void Awake()
            {
                uiDocument = GetComponent<UIDocument>();
                root = uiDocument.rootVisualElement;
            }
        }
    }

namespace Photon.Pun
{
    public class MonoBehaviourPunBase : MonoBehaviourPun
    {
        bool isMine() => photonView.IsMine || SO_DependencyManager.Instance.CreatePlayerOffline;

        // void OnEnable()
        // {
        //     if (isMine())
        //         MyOnEnable();
        // }
        // protected virtual void MyOnEnable() { }

        void Awake()
        {
            if (isMine())
                MyAwake();
        }
        protected virtual void MyAwake() { }

        // void Start() {
        //     if (!isMine())
        //         MyStart();
        // }
        // protected virtual void MyStart(){}

        void Update()
        {
            if (isMine())
                MyUpdate();
        }
        protected virtual void MyUpdate() { }
    }
}

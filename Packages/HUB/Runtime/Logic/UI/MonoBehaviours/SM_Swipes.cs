using UnityEngine;
using SwipeMenu;

[CreateAssetMenu(fileName = "SO_Swipes", menuName = "UI/SO_Swipes")]
public class SM_Swipes : ScriptableObject
{
    public void SwipeRight() => SwipeRight(1);
    public void SwipeLeft() => SwipeLeft(1);
    public void SwipeRight(int r) => MoveTo(SwipeDirection.Right, r);
    public void SwipeLeft(int r) => MoveTo(SwipeDirection.Left, r);
    public void MoveTo(SwipeDirection dir, int range = 1)
    {
        range = Mathf.Clamp(range, 1, 10); // what's infinity?
        int to = dir == 0 ? -1 : 1; //TODO:
        Menu.instance.MoveLeftRightByAmount(to * range);
    }
}
public enum SwipeDirection
{
    Left,
    Right
}

using UnityEngine;

public class LifeBar2D : MonoBehaviour
{
    public Transform lifeBar;
    private LifeSystem lifeSystem;

    private void Start()
    {
        lifeSystem = GetComponentInParent<LifeSystem>();
    }

    private void Update()
    {
        var lifePercentage = lifeSystem.LifePercentage;
        lifeBar.localScale = lifeBar.localScale.Update(x: 1 * lifePercentage);
    }
}
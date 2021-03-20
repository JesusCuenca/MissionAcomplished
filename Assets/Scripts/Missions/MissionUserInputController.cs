using UnityEngine;

public class MissionUserInputController : MonoBehaviour
{
    public delegate void MissionClicked(GameObject gameObject);
    public static event MissionClicked missionClicked;

    // Update is called once per frame
    void Update()
    {
        DetectMouseClick();
    }

    private void DetectMouseClick() {
        if (missionClicked != null && Input.GetMouseButtonDown(0))
        {
            Vector3 imp = Input.mousePosition;
            Vector3 mousePosition2d = new Vector3(imp.x, imp.y, -10);
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mousePosition2d);

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(imp), Vector2.zero);
            if (!hit) return;

            if (hit.collider.CompareTag("Mission"))
            {
                missionClicked(hit.collider.gameObject);
            }
        }
    }
}

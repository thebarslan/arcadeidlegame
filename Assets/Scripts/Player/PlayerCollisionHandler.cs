using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerCollisionHandler : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Source"))
            {
                other.GetComponent<Source>().CoroutineStart();
            }
            if (other.gameObject.CompareTag("Tree"))
            {
                if (other.transform.parent.transform.parent.GetComponent<Tree>().cutCount > 0)
                {
                    other.transform.parent.transform.parent.GetComponent<Tree>().CutTree();
                    transform.GetComponent<Animator>().SetBool("isCutting", true);
                    Debug.Log("Cutting");
                }
            }
            if (other.gameObject.CompareTag("CraftingTable"))
            {
                FindObjectOfType<Crafting_Manager>().GetComponent<Crafting_Manager>().OpenCraftfingPanel();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Source"))
            {
                other.GetComponent<Source>().CoroutineStop();
            }
            if (other.gameObject.CompareTag("Tree"))
            {
                other.transform.parent.transform.parent.GetComponent<Tree>().CutTreeStop();
                transform.GetComponent<Animator>().SetBool("isCutting", false);
                Debug.Log("CuttingExit");
            }
            if (other.gameObject.CompareTag("CraftingTable"))
            {
                FindObjectOfType<Crafting_Manager>().GetComponent<Crafting_Manager>().CloseCraftingPanel();
            }
        }
    }
}

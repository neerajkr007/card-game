using UnityEngine;

public class name_check : MonoBehaviour
{
    public TMPro.TMP_InputField name;
    public GameObject tobedisabled;
    public GameObject tobeenabled1;
    public GameObject tobeenabled2;

    public void check()
    {
        if (name.text == "")
        {
            tobedisabled.SetActive(false);
            tobeenabled1.SetActive(true);
        }
        else
        {
            tobedisabled.SetActive(false);
            tobeenabled2.SetActive(true);
        }

    }
}

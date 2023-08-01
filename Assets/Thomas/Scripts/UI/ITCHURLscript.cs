using UnityEngine;

public class ITCHURLscript : MonoBehaviour
{
    public string ThomasURL, JuanURL, RioURL, CamronURL;

    public void ThomasOpen()
    {
        Application.OpenURL(ThomasURL);
    }

    public void JuanOpen()
    {
        Application.OpenURL(JuanURL);
    }

    public void RioOpen()
    {
        Application.OpenURL(RioURL);
    }

    public void CamronOpen()
    {
        Application.OpenURL(CamronURL);
    }
}

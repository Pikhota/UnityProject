using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View
{
    public class ProgressBarWithText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txt;
        [SerializeField] private Image background;

        public void Fill(int current, int max)
        {
            txt.text = string.Format("{0}/{1}", current, max);
            background.fillAmount = (float) current / (float) max;
        }
    }
}
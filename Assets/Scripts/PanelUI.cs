using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelUI : MonoBehaviour
{
    [SerializeField] Transform StartPos, EndPos, TransferCount, Path, Length;

	public void UpdateValues(string start, string target, int count, Path path, int length)
	{
        StartPos.GetComponent<TextMeshProUGUI>().text = start;
        EndPos.GetComponent<TextMeshProUGUI>().text = target;
        TransferCount.GetComponent<TextMeshProUGUI>().text = count.ToString();
        Path.GetComponent<TextMeshProUGUI>().text = path.ToString();
        Length.GetComponent<TextMeshProUGUI>().text = length.ToString();
    }
}

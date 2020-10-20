using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
	public static Text changeCharacterObj;
	public static Text healthObj;
	public static Text attackObj;

	internal static void RemoveCharacterText() => changeCharacterObj.enabled = false;
	internal static void RemoveHealthText() => healthObj.enabled = false;
	internal static void RemoveAttackText() => attackObj.enabled = false;
}

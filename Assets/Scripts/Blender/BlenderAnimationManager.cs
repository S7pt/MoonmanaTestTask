using System.Collections;
using UnityEngine;

namespace MoonmanaTestTask.Blender
{
	public class BlenderAnimationManager : MonoBehaviour
	{
		private const string MIXING_TRIGGER = "Mixing";
		private const string LID_OPEN_TRIGGER = "Open";
		private const string FILLING_TRIGGER = "Fill";
		private const string LEVEL_BEGIN_TRIGGER = "LevelBegin";
		private const float MIXING_ANIMATIONS_DELAY = 1.5f;
		private const float MIXING_END_DELAY = 0.5f;

		private readonly int MIXING_HASH = Animator.StringToHash(MIXING_TRIGGER);
		private readonly int LID_OPEN_HASH = Animator.StringToHash(LID_OPEN_TRIGGER);
		private readonly int FILLING_TRIGGER_HASH = Animator.StringToHash(FILLING_TRIGGER);
		private readonly int LEVEL_TRIGGER_HASH = Animator.StringToHash(LEVEL_BEGIN_TRIGGER);

		[SerializeField] private Animator _blenderAnimator;
		[SerializeField] private Animator _blenderLidAnimator;
		[SerializeField] private Animator _mixtureAnimator;
		[SerializeField] private BlenderMixture _mixtureVisuals;

		public void Setup()
		{
			_mixtureAnimator.ResetTrigger(FILLING_TRIGGER_HASH);
			_mixtureAnimator.SetTrigger(LEVEL_TRIGGER_HASH);
			OpenLid();
			_mixtureVisuals.Setup();
		}

		public void CloseLid()
		{
			_blenderLidAnimator.ResetTrigger(LID_OPEN_HASH);
			_blenderLidAnimator.SetTrigger(MIXING_HASH);
		}

		public void OpenLid()
		{
			_blenderLidAnimator.ResetTrigger(MIXING_HASH);
			_blenderLidAnimator.SetTrigger(LID_OPEN_HASH);
		}

		public IEnumerator StartMixingAnimation(Color mixtureColor)
		{
			yield return StartMixing(mixtureColor);
		}

		public void Fill()
		{
			_mixtureAnimator.ResetTrigger(LEVEL_TRIGGER_HASH);
			_mixtureAnimator.SetTrigger(FILLING_TRIGGER_HASH);
		}

		public void ShakeBlender()
		{
			_blenderAnimator.SetTrigger(MIXING_HASH);
		}

		private IEnumerator StartMixing(Color mixtureColor)
		{
			CloseLid();
			yield return new WaitForSeconds(MIXING_ANIMATIONS_DELAY);
			Fill();
			_mixtureVisuals.Fill(mixtureColor);
			ShakeBlender();
			yield return new WaitForSeconds(MIXING_ANIMATIONS_DELAY);
			OpenLid();
			yield return new WaitForSeconds(MIXING_END_DELAY);
		}
	}
}
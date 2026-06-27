using UnityEngine;

namespace Runtime.Audio
{
    public class MusicSwap : MonoBehaviour
    {
        [SerializeField] AudioSource tape1;
        [SerializeField] AudioSource tape2;

        private int currentTape = 1;

        private void Awake()
        {
            Play1();

            currentTape = 1;
        }


        public void SwapTapes()
        {
            if (currentTape == 1)
                Play2();
            else 
                Play1();
        }

        private void Play1()
        {
            tape2.Stop();
            tape1.Play(); 
        }

        private void Play2()
        {
            tape1.Stop();
            tape2.Play();
        }
    }
}



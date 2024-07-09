    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class NPCMovimentation : MonoBehaviour
    {
        public bool readyToGo;
        public bool inMoviment;
        [Range(0, 5)]
        public float movimentVelocity;

        public Rigidbody2D rb;
        public List<Transform> points;

        public Transform currentTarget;
        public Transform lastTarget;
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();   
        }
        private void Update()
        {
            if (readyToGo)
            {
                Moviment();
            }
            else { return; }
           
        }

    public void Moviment()
    {
        if (!inMoviment)
        {
            Shuffle(points);
            foreach (Transform t in points)
            {
                if (t != currentTarget)  // Verifica se o alvo atual não é o mesmo que o último
                {
                    currentTarget = t;
                    if (currentTarget != null)
                    {
                        StartCoroutine(MoveToTargets());
                        break;  // Interrompe o loop assim que um novo alvo for selecionado
                    }
                }
            }
        }
    }
    private IEnumerator MoveToTargets()
        {
            while (readyToGo == true)
            {
                inMoviment = true;
                if (currentTarget != null)
                {
                    transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, movimentVelocity * Time.deltaTime);
                }
                yield return null;
                inMoviment = false;
            }
        }

    public void NextPointAfterAction(Transform? t)
    {
        if (t == null)
        {
            currentTarget = null;
            readyToGo = true;
            inMoviment = false;
        }
        else
        {
            currentTarget = null;
            readyToGo = true;
            inMoviment = false;
            currentTarget = t;
        }
    }


    public void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1); // Random.Range do Unity para gerar um número aleatório entre 0 e n inclusive
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

using UnityEngine;

public class BossAnimation : MonoBehaviour
{
   private Boss boss;

   private void Awake()
    {
        boss = GetComponentInParent<Boss>();
    }

    public void FinalizarAtaque()
    {
        boss.FinalizarAtaque();
    }

    public void CausarDano()
    {
        boss.CausarDano();
    }

    public void CriarRajada()
    {
        boss.CriarRajada();
    }
}

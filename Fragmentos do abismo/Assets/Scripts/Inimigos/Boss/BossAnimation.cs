using UnityEngine;

public class BossAnimation : MonoBehaviour
{
   private Boss boss;
   public GameObject Explosao; // PreBab Explosao

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
        boss.CausarDanoFacao();
    }

    public void CriarRajada()
    {
        boss.CriarRajada();
    }

    public void RepelirPlayer()
    {
        boss.RepelirPlayer();
    }

    public void AtivarExplosao()
    {
        Explosao.SetActive(true);
    }
    
    public void DesativarExplosao()
    {
        Explosao.SetActive(false);
    }

    public void AtivarInvencibilidade()
    {
        boss.AtivarIvencibilidade();
    }

    public void DesativarInvencibilidade()
    {
        boss.DesativarIvencibilidade();
    }

    public void AbrirBarreira()
    {
        boss.AbrirBarreira();
    }

    public void FinalizarDano()
    {
        boss.FinalizarDano();
    }

    public void CausarDanoInvestida()
    {
        boss.CausarDanoInvestida();
    }
    
}

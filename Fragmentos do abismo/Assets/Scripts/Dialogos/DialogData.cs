using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public struct Dialogo
{
    [TextArea(5, 10)]
    public string texto;
    
}

[CreateAssetMenu(fileName = "DialogData", menuName = "ScriptableObject/TalkScript", order = 1)]
public class DialogData : ScriptableObject
{
   public List<Dialogo> talkScript;
}

// aqui basicamente cria uma estrutura de dados para armazenar diálogos, onde cada diálogo é representado por uma string
// A classe DialogData é um ScriptableObject que contém uma lista de diálogos (talkScript) que podem ser usados em diferentes partes do jogo.
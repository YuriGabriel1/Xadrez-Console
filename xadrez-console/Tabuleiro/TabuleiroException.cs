using System;

namespace Tabuleiro;

public class TabuleiroException : Exception // exeção  personalizada
{
    public TabuleiroException(string msg) : base(msg)
    {
        
    }
}
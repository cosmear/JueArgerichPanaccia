using System;

class Font
{
    

    IntPtr pointer;

    
    public Font(string fileName, short size)
    {
        Load(fileName, size);
    }

    public void Load(string fileName, short size)
    {
        pointer = Engine.LoadFont(fileName, size);
        if (pointer == IntPtr.Zero)
            Engine.ErrorFatal("Fuente inexistente: " + fileName);
    }

    public IntPtr ReadPointer()
    {
        return pointer;
    }

}

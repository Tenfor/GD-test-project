using System;
[Serializable]
public struct GridPosition
{
    public GridPosition(int r, int c){
        row = r;
        col = c;
    }
    public int row;
    public int col;
}

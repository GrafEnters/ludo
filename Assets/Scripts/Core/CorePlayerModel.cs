public class CorePlayerModel {
    public string Name;
    public int[] Pawns;

    public CorePlayerModel(string playerName) {
        Name = playerName;
        Pawns = new[] { 0, 0, 0, 0 };
    }
    
    public void KillPawnsInCell(int cell) {
        for (int i = 0; i < Pawns.Length; i++) {
            if (i == cell) {
                i = 0;
            }
        }
    }
}
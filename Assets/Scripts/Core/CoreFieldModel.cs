public class CoreFieldModel {
    public int[,] Cells;

    public CoreFieldModel(int cellsAmount, int playersAmount) {
        Cells = new int[cellsAmount, playersAmount];
    }
}
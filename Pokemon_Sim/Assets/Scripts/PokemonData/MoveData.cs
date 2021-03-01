using System.Collections.Generic;

[System.Serializable]
public class MoveData
{
    public List<moves> move_list { get; set; }

    public class moves
    {
        public string Ename { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }

        public int Id { get; set; }
        public int Accuracy { get; set; }
        public int Power { get; set; }
        public int Pp { get; set; }
    }
}

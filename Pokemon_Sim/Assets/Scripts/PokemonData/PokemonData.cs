using System.Collections.Generic;


public class PokemonData
{
    public int id { get; set; }
    public pkmn_name Name { get; set; }
    public List<pkmn_type> type { get; set; }
    public pkmn_base base_stats { get; set; }

    #region Classes
    public class pkmn_name
    {
        public string english { get; set; }
        public string japanese { get; set; }
        public string chinese { get; set; }
        public string french { get; set; }
    }

    public class pkmn_type
    {
        public int type_0 { get; set; }
        public int type_1 { get; set; }
    }

    public class pkmn_base
    {
        public float hp { get; set; }
        public float attack { get; set; }
        public float defense { get; set; }
        public float sp_attack { get; set; }
        public float sp_defense { get; set; }
        public float speed { get; set; }
    }

    #endregion
}

using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ExperienceTable", menuName = "ExperienceTable", order = 0)]
    public class ExperienceTable : ScriptableObject
    {
        [SerializeField] private List<ExperienceData> _experienceDatas;

        public List<ExperienceData> Datas => _experienceDatas;
    }
}
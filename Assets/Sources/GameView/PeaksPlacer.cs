using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sources.GameView
{
    public class PeaksPlacer : MonoBehaviour
    {
        [Min(1)]
        [SerializeField] private int _size;
        [SerializeField] private float _rowDistanceBetween;
        [SerializeField] private float _columnDistanceBetween;
        [SerializeField] private List<Peak> _peaks;

        public IEnumerable<Peak> Peaks => _peaks;

        private void OnValidate()
        {
            _peaks = DeleteDuplicates(_peaks);
            PlacePeaks();
            InitPeaks();
        }

        private void Start()
        {
            InitPeaks();
        }

        private List<Peak> DeleteDuplicates(IEnumerable<Peak> transforms)
        {
            List<Peak> uniqueTransforms = new List<Peak>();

            if (transforms != null)
            {
                foreach (Peak child in transforms)
                {
                    if (uniqueTransforms.Contains(child) == false)
                        uniqueTransforms.Add(child);
                }
            }

            return uniqueTransforms;
        }

        private void PlacePeaks()
        {
            float multiplier = (_size - 1) / 2f;
            float startX = transform.position.x - multiplier * _columnDistanceBetween;
            float startY = transform.position.y;
            float startZ = transform.position.z - multiplier * _rowDistanceBetween;

            int childIndex = 0;
            for (int row = 0; row < _size; row++)
            {
                for (int column = 0; column < _size; column++)
                {
                    float z = startZ + row * _rowDistanceBetween;
                    float x = startX + column * _columnDistanceBetween;

                    Vector3 position = new Vector3(x, startY, z);
                    
                    if (childIndex >= _peaks.Count)
                        return;
                    
                    _peaks[childIndex].transform.position = position;
                    childIndex++;
                }
            }
        }

        private void InitPeaks()
        {
            int childIndex = 0;
            for (int row = 0; row < _size; row++)
            {
                for (int column = 0; column < _size; column++)
                {
                    if (childIndex >= _peaks.Count)
                        return;
                    
                    _peaks[childIndex].Init(row, column);
                    childIndex++;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using DataStructures.Hashing;

namespace DataStructures.Dictionaries
{
    class BloomFilter<T>
    {
        private BitArray _bitArray { get; set; }
        private UniversalHashingFamily _universalHashingFamily { get; set; }

        private void _AddElem(T elem)
        {
            for (int hashFuncIndex = 1; hashFuncIndex < _universalHashingFamily.NumberOfFunctions; ++hashFuncIndex)
            {
                int hashVal = _universalHashingFamily.UniversalHash(elem.GetHashCode(), hashFuncIndex);
                int index = Math.Abs(hashVal) % _bitArray.Length;

                _bitArray[index] = true;
            }
        }

        private bool _ElemDoesNotExist(T elem)
        {
            for (int hashFuncIndex = 1; hashFuncIndex < _universalHashingFamily.NumberOfFunctions; ++hashFuncIndex)
            {
                int hashVal = _universalHashingFamily.UniversalHash(elem.GetHashCode(), hashFuncIndex);
                int index = Math.Abs(hashVal) % _bitArray.Length;

                if (!_bitArray[index])
                    return true;
            }

            return false;
        }

        public BloomFilter(int numOfBits, int numOfHashFuncs)
        {
            _bitArray = new BitArray(numOfBits, false);
            _universalHashingFamily = new UniversalHashingFamily(numOfHashFuncs);
        }

        public void Add(T elem)
        {
            _AddElem(elem);
        }

        public bool QueryNonExistance(T elem)
        {
            return _ElemDoesNotExist(elem);
        }
    }
}

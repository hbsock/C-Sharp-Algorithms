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

        private void _AddNonExistantElem(T elem)
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

                if (_bitArray[index])
                    return false;
            }

            return true;
        }

        public BloomFilter(int numOfBits, int numOfHashFuncs)
        {
            _bitArray = new BitArray(numOfBits, false);
            _universalHashingFamily = new UniversalHashingFamily(numOfHashFuncs);
        }

        public void Add(T elem)
        {
            if(_ElemDoesNotExist(elem))
            {
                _AddNonExistantElem(elem);
            }
            else
            {
                throw new NotImplementedException("Have not implemented checking if element does exist to avoid adding duplicates!");
            }
        }

        public bool QueryNonExistance(T elem)
        {
            return _ElemDoesNotExist(elem);
        }
    }
}

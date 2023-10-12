namespace Find_In_Mountain_Array
{
    internal class Program
    {
       
        static void Main(string[] args)
        {
            Solution s= new Solution();
            var answer = s.FindInMountainArray(3, new MountainArray());
        }



        // This is MountainArray's API interface.
        // You should not implement it, or speculate about its implementation
        class MountainArray : IMountainArray
        {
            int[] arr = new int[] { 0, 1, 2, 4, 2, 1 }; // another example [1,2,3,4,5,3,1], target = 3
            public int Get(int index) { return arr[index]; }
            public int Length() { return arr.Length; }
        }

        public interface IMountainArray
        {
            public int Get(int index);
            public int Length();
        }


        class Solution
        {
            public int FindInMountainArray(int target, MountainArray mountainArr)
            {
                // 0,1,2,4,2,1
                /*
                Step 1 - find the peak element, use binary search. mountainArr.get(i - 1) < mountainArr.get(i) > mountainArr.get(i+1).
                Step 2 - Once we know the peak element, left of all peak elements are ordered in ASC, use BS to find target., If found here we can return, as we need to return the minimum index.
                Step 3 - right of all peak elements are ordered in DSC, use BS to find target.  
                */
                int length = mountainArr.Length();
                int l = 1; int r = length - 2; // why started from 1 to length - 2 ? As we are searching for the peak element, and this wont be present at the edges.
                                               // Step 1 
                int m = 0;
                while (l <= r)
                {
                    m = l + (r - l) / 2;
                    int lEle = mountainArr.Get(m - 1);//3
                    int mEle = mountainArr.Get(m); // 4
                    int rEle = mountainArr.Get(m + 1); // 5
                    if (lEle < mEle && mEle < rEle)
                        l = m + 1;
                    else if (lEle > mEle && mEle > rEle)
                        r = m - 1;
                    else break;
                }

                int peak = m;
                // Step 2
                l = 0; r = m;
                while (l <= r)
                {
                    m = l + (r - l) / 2;
                    int mEle = mountainArr.Get(m);
                    if (mEle < target)
                    {
                        l = m + 1;
                    }
                    else if (mEle > target)
                    {
                        r = m - 1;
                    }
                    else return m;
                }

                // Step 3
                l = peak; r = length - 1;
                while (l <= r)
                {
                    m = l + (r - l) / 2;
                    int mEle = mountainArr.Get(m);
                    if (mEle > target)
                    {
                        l = m + 1;
                    }
                    else if (mEle < target)
                    {
                        r = m - 1;
                    }
                    else return m;
                }

                return -1;
            }
        }
    }
}
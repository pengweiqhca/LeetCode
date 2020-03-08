using System.Collections.Generic;

namespace LeetCode.P7
{
    public class LeetCode735
    {
        public int[] AsteroidCollision(int[] asteroids)
        {
            var list = new LinkedList<int>(asteroids);

            var current = list.First;
            while (current?.Next != null)
            {
                if (current.Next == null) break;

                if (current.Value > 0 && current.Next.Value < 0)
                {
                    var diff = current.Value + current.Next.Value;
                    if (diff < 0)
                    {
                        if (current.Previous == null)
                        {
                            list.RemoveFirst();

                            current = list.First;
                        }
                        else
                        {
                            var previous = current.Previous;

                            list.Remove(current);

                            current = previous;
                        }
                    }
                    else if (diff > 0)
                    {
                        if (current.Next.Next == null)
                        {
                            list.RemoveLast();

                            break;
                        }

                        list.Remove(current.Next);
                    }
                    else
                    {
                        if (current.Previous == null)
                        {
                            list.RemoveFirst();
                            list.RemoveFirst();

                            current = list.First;
                        }
                        else
                        {
                            var previous = current.Previous;

                            list.Remove(current.Next);
                            list.Remove(current);

                            current = previous;
                        }
                    }
                }
                else current = current.Next;
            }

            asteroids = new int[list.Count];

            var index = 0;
            current = list.First;
            while (current != null)
            {
                asteroids[index++] = current.Value;
                current = current.Next;
            }

            return asteroids;
        }
    }
}

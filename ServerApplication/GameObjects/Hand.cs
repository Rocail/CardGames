using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApplication.GameObjects
{
    class Hand
    {
        public ArrayList CardManagers = new ArrayList();

        public ArrayList GetCardModels()
        {
            ArrayList cardModels = new ArrayList();
            foreach (CardManager cardManager in CardManagers)
            {
                cardModels.Add(cardManager.Card);
            }
            return cardModels;
        }

        override public string ToString()
        {
            var message = "";
            if (CardManagers.Count == 0 )
            {
                return "no cards";
            } else
            {
                foreach (CardManager cardManager in CardManagers)
                {
                    message += cardManager.Card.ToString() + "\n";
                }
                return message;
            }
        }
    }
}

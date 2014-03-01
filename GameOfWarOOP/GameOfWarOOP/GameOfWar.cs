using System;
using System.Collections.Generic;

namespace GameOfWarOOP
{
    class GameOfWar
    {
        private Player _player1 = new Player();
        private Player _player2 = new Player();
        private CardCollection _gameDeck = new CardCollection(52);
        private bool _isGameRunning = true;

        public void PlayGame()
        {
            PromptForNames();
            DealHands();
            GameLoop();
            SaveGameStats();
            DisplayVictory();
        }

        private void PromptForNames()
        {
            Console.WriteLine("Input Name for Player 1:");
            _player1.Name = Console.ReadLine();
            Console.WriteLine("Input Name for Player 2:");
            _player2.Name = Console.ReadLine();
        }

        private void DealHands()
        {
            _gameDeck.Shuffle();
            int size = _gameDeck.Count;
            for (int i = 0; i < size / 2; i++)
            {
                _player1.AddCardToHand(_gameDeck.RemoveCard());
                _player2.AddCardToHand(_gameDeck.RemoveCard());
            }
        }

        private void GameLoop()
        {
            while (_isGameRunning)
            {
                Skirmish();
                _isGameRunning = !HasVictoryOccured();
            }

        }

        private bool HasVictoryOccured()
        {
            if (_player1.Score == 0 || _player2.Score == 0) return true;
            return false;
        }

        private void Skirmish()
        {
            Console.WriteLine("A skirmish begins!");
            Rehand(1);
            AddCardToSpoils();

            Console.Write(_player1.Name + " plays " + _player1.ActiveSpoil.FullName);
            Console.WriteLine();
            Console.Write(_player2.Name + " plays " + _player2.ActiveSpoil.FullName);
            Console.WriteLine();

            EvaluateOutcome();
        }

        private void EvaluateOutcome()
        {
            //Player 2 wins
            if(_player1.ActiveSpoil.CompareTo(_player2.ActiveSpoil) == -1)
            {
                //move cards from spoils
                _player2.CollectSpoils(_player1);

                //display outcome
                Console.WriteLine(_player2.Name + " won the skirmish!");
                Console.WriteLine("Current Score:");
                Console.WriteLine(_player1.Name + ": " + _player1.Score + "\t\t" + _player2.Name + ": " + _player2.Score);
            }
            //Player 1 wins
            else if (_player1.ActiveSpoil.CompareTo(_player2.ActiveSpoil) == 1)
            {
                //move cards from spoils
                _player1.CollectSpoils(_player2);

                //display outcome
                Console.WriteLine(_player1.Name + " won the skirmish!");
                Console.WriteLine("Current Score:");
                Console.WriteLine(_player1.Name + ": " + _player1.Score + "\t\t" + _player2.Name + ": " + _player2.Score);
            }
            else
            {
                Battle();
            }
        }

        private void Battle()
        {
            //Battle has occured
            Console.WriteLine("A battle has occurred!");
            //Check deadman's hand condition
            if (IsDeadMansHand())
            {
                //player 1 dead mans
                if (_player1.CardsLeftToDraw == 0 && _player2.CardsLeftToDraw != 0) DeadMansHand(_player1);
                //Player 2 dead mans
                else if (_player1.CardsLeftToDraw != 0 && _player2.CardsLeftToDraw == 0) DeadMansHand(_player2);
                //Double dead mans
                else DeadMansHand();
            }
            else
            {
                //get bounty size -> determined by number of cards that each! player has
                int bounty = GetBountySize();
                //Rehand based on bounty size
                Rehand(bounty);
                //Add cards to spoils
                AddCardsToSpoils(bounty);
                //Skirmish
                Skirmish();
            }
        }

        private void DeadMansHand(Player losingPlayer)
        {
            Console.WriteLine(losingPlayer.Name + " has drawn a Dead Man's Hand!");
            _player1.CollectSpoils(losingPlayer);
        }

        private void DeadMansHand()
        {
            Console.WriteLine("A Double Dead Man's Hand has occurred!");
            _player1.ClearSpoils();
            _player2.ClearSpoils();
        }

        private bool IsDeadMansHand()
        {
            if (_player1.CardsLeftToDraw == 0 || _player2.CardsLeftToDraw == 0) return true;
            return false;
        }

        private int GetBountySize()
        {
            if (_player1.PossibleBounty < _player2.PossibleBounty) return _player1.PossibleBounty;
            return _player2.PossibleBounty;
        }

        private void SaveGameStats()
        {
        }

        private void DisplayVictory()
        {
            if (_player1.Score != 0 && _player2.Score == 0)
            {
                Console.WriteLine(_player1.Name + " won the game!");
            }
            else if (_player1.Score == 0 && _player2.Score != 0)
            {
                Console.WriteLine(_player1.Name + " won the game!");
            }
            else
            {
                Console.WriteLine("The Game of War has ended in a draw. In war there are no winners.");
            }
        }

        private void Rehand(int numCards)
        {
            _player1.RehandIfNecessary(numCards);
            _player2.RehandIfNecessary(numCards);
        }

        private void AddCardToSpoils()
        {
            _player1.AddCardToSpoils(_player1.PlayCardFromHand());
            _player2.AddCardToSpoils(_player2.PlayCardFromHand());
        }

        private void AddCardsToSpoils(int numCards)
        {
            for (int i = 0; i < numCards; i++)
            {
                AddCardToSpoils();
            }
        }
    }
}

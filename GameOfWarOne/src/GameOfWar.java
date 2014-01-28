import java.util.ArrayList;
import java.util.Random;
import java.util.Scanner;

public class GameOfWar {
	public static void main(String[] args) {
		
		
		String playerOneName = "player one";
		String playerTwoName = "player two";
		int scoreOne = 0;
		int scoreTwo = 0;
		boolean isGameRunning = true;
		
		
		//Get Player Names
		/*write("Input Player One's Name: ");
		String playerOneName = getName();
		write("Player One's Name is: "+playerOneName);
		
		write("Input Player Two's Name: ");
		String playerTwoName = getName();
		write("Player One's Name is: "+playerTwoName);
		*/
		
		//Get Deck of Cards
		ArrayList<Integer> deck = new ArrayList<Integer>();
		deck = getCards(deck);
		//Shuffle Deck
		shuffle(deck);
		
		//Deal cards to player hands
		ArrayList<Integer> handOne = new ArrayList<Integer>();
		ArrayList<Integer> handTwo = new ArrayList<Integer>();
		
		dealCards(deck, handOne, handTwo);
		
		/* Create Rigged decks to test double dead man's hand condition
		handOne.clear();
		handOne = getRiggedDeck(handOne);
		handTwo.clear();
		handTwo = getRiggedDeck(handTwo);
		*/
		
		//Create discard piles
		ArrayList<Integer> discardOne = new ArrayList<Integer>();
		ArrayList<Integer> discardTwo = new ArrayList<Integer>();
		
		//Create spoils piles
		ArrayList<Integer> spoilsOne = new ArrayList<Integer>();
		ArrayList<Integer> spoilsTwo = new ArrayList<Integer>();
		
		/*write("Player One Hand");
		for(int card:handOne){write(Integer.toString(card));}
		
		write("Player Two Hand");
		for(int card:handTwo){write(Integer.toString(card));}
		*/
		
		//Game Loop
		while(isGameRunning) {
			//Begin Skirmish
			
			//Check to see if players need to rehand
			rehandIfNecessary(handOne,discardOne,1);
			rehandIfNecessary(handTwo,discardTwo,1);
			
			//Players play card
			spoilsOne.add(handOne.remove(0)); 	//removes card from "top" of deck, this is important
			spoilsTwo.add(handTwo.remove(0));	//because when we rehand we add to the "bottom" of the deck
			
			//Print cards played
			write(playerOneName+" plays "+intToCardString(spoilsOne.get(spoilsOne.size()-1)));
			write(playerTwoName+" plays "+intToCardString(spoilsTwo.get(spoilsTwo.size()-1)));
			
			//Evaluate outcome
			int activeCardOne = spoilsOne.get(spoilsOne.size()-1);
			int activeCardTwo = spoilsTwo.get(spoilsTwo.size()-1);
			
			if((activeCardOne%13)>(activeCardTwo%13)) {
				//player one wins
				//add spoils to p1 discard and clear spoils
				discardOne.addAll(spoilsOne);
				discardOne.addAll(spoilsTwo);
				spoilsOne.clear();
				spoilsTwo.clear();
				
				//Add scores
				scoreOne = handOne.size()+discardOne.size();
				scoreTwo = handTwo.size()+discardTwo.size();
				
				//display winner and scores
				write(playerOneName+" won the skirmish.");
				write("Current Score:");
				write(playerOneName+": "+scoreOne+"\t\t"+playerTwoName+": "+scoreTwo);
				
			} else if((activeCardOne%13)<(activeCardTwo%13)){
				//player two wins
				//add spoils to p2 discard and clear spoils
				discardTwo.addAll(spoilsOne);
				discardTwo.addAll(spoilsTwo);
				spoilsOne.clear();
				spoilsTwo.clear();
				
				//Add scores
				scoreOne = handOne.size()+discardOne.size();
				scoreTwo = handTwo.size()+discardTwo.size();
				
				//display winner and scores
				write(playerTwoName+" won the skirmish.");
				write("Current Score:");
				write(playerOneName+": "+scoreOne+"\t\t"+playerTwoName+": "+scoreTwo);
				
			} else {
				// Begin battle
				write("A battle has begun!");
				// Check for dead man's hands
				
				// player one victory
				if(handOne.size()+discardOne.size()!=0 && handTwo.size()+discardTwo.size()==0) {
					// move spoils to player one discard
					discardOne.addAll(spoilsOne);
					discardOne.addAll(spoilsTwo);
					spoilsOne.clear();
					spoilsTwo.clear();
					
					//Alert players of dead man's hand
					write("There was a dead man's hand!");
					
					// set scores
					scoreOne = handOne.size()+discardOne.size();
					scoreTwo = handTwo.size()+discardTwo.size();
					
					// end game
					isGameRunning = false;
					continue;
					
					//player two victory
				} else if(handOne.size()+discardOne.size()==0 && handTwo.size()+discardTwo.size()!=0) {
					// move spoils to player one discard
					discardTwo.addAll(spoilsOne);
					discardTwo.addAll(spoilsTwo);
					spoilsOne.clear();
					spoilsTwo.clear();
					
					//Alert players of dead man's hand
					write("There was a dead man's hand!");
					
					// set scores
					scoreOne = handOne.size()+discardOne.size();
					scoreTwo = handTwo.size()+discardTwo.size();
					
					// end game
					isGameRunning = false;
					continue;
					
					//double dead man's hand!
				} else if(handOne.size()+discardOne.size()==0 && handTwo.size()+discardTwo.size()==0) {
					
					//Alert players of dead man's hand
					write("There was a double dead man's hand!");
					
					// set scores
					scoreOne = 0;
					scoreTwo = 0;
					
					// end game
					isGameRunning = false;
					continue;
				} 
				
				// Check for bounty count
				int bountyCount = 3;
				int availOne = handOne.size()+discardOne.size();
				int availTwo = handTwo.size()+discardTwo.size();
				// if(available cards  < 4) then bounty count = available cards - 1 (for the next skirmish)
				if(availOne<4 || availTwo<4) {
					if(availOne<availTwo) {
						bountyCount = availOne-1;
					} else {
						bountyCount = availTwo-1;
					}
				}
				
				// check for rehand
				rehandIfNecessary(handOne, discardOne, bountyCount);
				rehandIfNecessary(handTwo, discardTwo, bountyCount);
				
				// add bounty count to spoils piles
				for(int i = 0; i < bountyCount; i++){
					spoilsOne.add(handOne.remove(0));
					spoilsTwo.add(handTwo.remove(0));
				}
				
				write("There were " + bountyCount + " bounty cards added to the spoils.");
				
				//set scores
				scoreOne = handOne.size()+discardOne.size()+spoilsOne.size();
				scoreTwo = handTwo.size()+discardTwo.size()+spoilsTwo.size();
			}
		//Standard Victory Condition
		if(scoreOne == 0 || scoreTwo == 0) isGameRunning = false;
		}
		
		if(scoreOne==0 && scoreTwo!=0) {
			write("Congratulations! "+playerTwoName+" was victorious!");
		} else if(scoreOne!=0 && scoreTwo==0) {
			write("Congratulations! "+playerOneName+" was victorious!");
		} else if(scoreOne == 0 && scoreTwo == 0) {
			write("The game ends in a tie!");
		}
		
	}
	
	public static String intToCardString(int card) {
		String rank;
		String suit;
		
		switch(card%13) {
			case 0:	rank = "Two";
					break;
			case 1:	rank = "Three";
					break;
			case 2:	rank = "Four";
					break;
			case 3:	rank = "Five";
					break;
			case 4:	rank = "Six";
					break;
			case 5:	rank = "Seven";
					break;
			case 6:	rank = "Eight";
					break;
			case 7:	rank = "Nine";
					break;
			case 8:	rank = "Ten";
					break;
			case 9:	rank = "Jack";
					break;
			case 10:	rank = "Queen";
						break;
			case 11:	rank = "King";
						break;
			case 12:	rank = "Ace";
						break;
			default:	rank = "";
						break;
		}
		
		switch((int)card/13) {
			case 0:	suit = "Clubs";
					break;
			case 1:	suit = "Diamonds";
					break;
			case 2:	suit = "Hearts";
					break;
			case 3:	suit = "Spades";
					break;
			default:	suit = "";
						break;
		}
		
		return rank+" of "+suit;
	}
	
	public static void write(String line){
		System.out.println(line);
	}
	
	public static String getName(){
		String name;
		Scanner in = new Scanner(System.in);
		name = in.next();
		in.close();
		return name;
	}
	
	//returns arraylist with 52 integers 0-51
	public static ArrayList<Integer> getCards(ArrayList<Integer> deck){
		for(int i = 0; i<52;i++){
			deck.add(i);
		}
		return deck;
	}
	
	//returns an unshuffled half deck
	public static ArrayList<Integer> getRiggedDeck(ArrayList<Integer> deck) {
		for(int i = 0; i < 26; i++) {
			deck.add(i);
		}
		return deck;
	}
	
	public static void shuffle(ArrayList<Integer> deck){
		int length = deck.size();
		Random rand = new Random(System.currentTimeMillis());
		rand.nextInt();
		for(int i = 0;i<length;i++){
			int change = i + rand.nextInt(length-i);
			swap(deck,i,change);
		}
	}
	
	public static void swap(ArrayList<Integer> deck, int a, int b){
		int temp = deck.get(a);
		deck.set(a, deck.get(b));
		deck.set(b, temp);
		
	}
	
	public static void dealCards(ArrayList<Integer> deck, ArrayList<Integer> a,
			ArrayList<Integer> b){
		int n = deck.size();
		for(int i = 0; i<n; i+=2){
			a.add(deck.get(i));
			b.add(deck.get(i+1));
		}
	}
	
	public static void rehandIfNecessary(ArrayList<Integer> hand, 
			ArrayList<Integer> discard, int numCards) {
		//Check to see if rehanding is necessary
		if(hand.size()>numCards) return;
		//Shuffle and add to the end of the hand
		shuffle(discard);
		for(int card:discard){
			hand.add(card);
		}
		discard.clear();
		write("Rehand occured");
	}
}

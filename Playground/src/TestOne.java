import java.util.ArrayList;


public class TestOne {
	public static void main(String[] args) {
		ArrayList<Integer> test1 = new ArrayList<Integer>();
		ArrayList<Integer> test2 = new ArrayList<Integer>();
		
		test1.add(1);
		test1.add(2);
		test1.add(3);
		test1.add(4);
		
		test2.add(0);
		
		System.out.println("Test1");
		for(int iter:test1) System.out.println(Integer.toString(iter));
		
		System.out.println("Test2");
		for(int iter:test2) System.out.println(Integer.toString(iter));
		
		System.out.println("\nTransfering Test1 to Test2...\n");
		
		test2.addAll(test1);
		test1.removeAll(test1);
		
		System.out.println("Test1");
		for(int iter:test1) System.out.println(Integer.toString(iter));
		
		modifyTest(test2);
		System.out.println("Test2");
		for(int iter:test2) System.out.println(Integer.toString(iter));
		
	}
	
	public static void modifyTest(ArrayList<Integer> test) {
		test.clear();
	}
}

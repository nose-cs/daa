from solution import smallest_dominating_set, greedy_dominating_set_V_plus_E
from tests import run_tests, generate_random_graph, compare_solutions


#graph = generate_random_graph(10)
#print(graph)

#print("Smallest Dominating Set (Subsets):", smallest_dominating_set(graph))

#print("Smallest Dominating Set (Greedy):", greedy_dominating_set_V_plus_E(graph))

# Run the test suite
#run_tests(5000)
compare_solutions(2000, compare_output=True)
from solution import smallest_dominating_set, greedy_dominating_set
from tests import run_tests, generate_random_graph


graph = generate_random_graph(10)
print(graph)

print("Smallest Dominating Set (Subsets):", smallest_dominating_set(graph))

print("Smallest Dominating Set (Greedy):", greedy_dominating_set(graph))

# Run the test suite
run_tests(5000)

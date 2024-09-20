import random
import math
from solution import smallest_dominating_set, greedy_dominating_set

MAX_GRAPH_SIZE = 50


def run_tests(count=1000):
    print("Starting the test suite...")
    for _ in range(0, count):
        n = random.randint(1, MAX_GRAPH_SIZE)
        graph = generate_random_graph(n)
        max_degree = get_max_degree(graph)
        solution = smallest_dominating_set(graph)
        approximation = greedy_dominating_set(graph)
        if not solution <= approximation <= math.ceil(math.log(n) + 1) * solution:
            print(f"Solution: {solution}")
            print(f"Approximation: {approximation}")
            print(f"Solution <= Approximation: {solution <= approximation}")
            print(f"ln(n) + 1: {math.ceil(math.log(n) + 1)}")
            print("Test failed.")
            print("Graph:", graph)
            print("Subsets solution:", solution)
            print("Greedy approximation:", approximation)
            raise Exception("Test failed.")
        if max_degree > 0 and approximation > (math.log(max_degree) + 2) * solution:
            print(f"Solution: {solution}")
            print(f"Approximation: {approximation}")
            print(f"ln(max_degree) + 2: {math.log(max_degree) + 2}")
            print("Graph:", graph)
            print("Subsets solution:", solution)
            print("Greedy approximation:", approximation)
            raise Exception("Test failed.")
    print("All tests completed successfully.")


def generate_random_graph(n, prob=0.5):
    graph = {i: [] for i in range(n)}
    for i in range(n):
        for j in range(i + 1, n):
            if random.random() < prob:
                graph[i].append(j)
                graph[j].append(i)
    return graph


def get_max_degree(graph):
    max_degree = 0
    for node in graph:
        max_degree = max(max_degree, len(graph[node]))
    return max_degree

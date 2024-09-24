import random
import math
import time
from solution import smallest_dominating_set, greedy_dominating_set, greedy_dominating_set_V_plus_E

MAX_GRAPH_SIZE = 50


def run_tests(count=1000):
    print("Starting the test suite...")
    for _ in range(0, count):
        n = random.randint(1, MAX_GRAPH_SIZE)
        graph = generate_random_graph(n)
        max_degree = get_max_degree(graph)
        solution = smallest_dominating_set(graph)
        approximation = greedy_dominating_set_V_plus_E(graph)
        if not solution <= approximation <= math.ceil(math.log(max_degree + 2)) * solution <= math.ceil(math.log(n) + 1) * solution:
            print(f"Solution: {solution}")
            print(f"Approximation: {approximation}")
            print(f"Solution <= Approximation: {solution <= approximation}")
            print(f"ln(n) + 1: {math.ceil(math.log(n) + 1)}")
            print(f"ln(max_degree + 2): {math.log(max_degree + 2)}")
            print("Test failed.")
            print("Graph:", graph)
            print("Subsets solution:", solution)
            print("Greedy approximation:", approximation)
            raise Exception("Test failed.")
    print("All tests completed successfully.")


def generate_random_graph(n, edge_prob=0.5):
    graph = {i: [] for i in range(n)}
    for i in range(n):
        for j in range(i + 1, n):
            if random.random() < edge_prob:
                graph[i].append(j)
                graph[j].append(i)
    return graph


def get_max_degree(graph):
    max_degree = 0
    for node in graph:
        max_degree = max(max_degree, len(graph[node]))
    return max_degree

def compare_solutions(count=1000, num_runs=5, compare_output=False):
    total_time_func1 = 0
    total_time_func2 = 0

    test_cases = [generate_random_graph(random.randint(1, MAX_GRAPH_SIZE)) for _ in range(count)]

    for i, test_case in enumerate(test_cases):
        output1 = None
        output2 = None

        start_time = time.time()
        for _ in range(num_runs):
            output1 = greedy_dominating_set(test_case)
        end_time = time.time()
        time_func1 = (end_time - start_time) / num_runs
        total_time_func1 += time_func1

        start_time = time.time()
        for _ in range(num_runs):
            output2 = greedy_dominating_set_V_plus_E(test_case)
        end_time = time.time()
        time_func2 = (end_time - start_time) / num_runs
        total_time_func2 += time_func2

        if compare_output and output1 != output2:
            print(f"Outputs differ on test case {i+1}")
            print(f"func1 output: {output1}")
            print(f"func2 output: {output2}")
        elif (i + 1) % 100 == 0:
            print(f"{i + 1} /{count}")

    avg_time_func1 = total_time_func1 / len(test_cases)
    avg_time_func2 = total_time_func2 / len(test_cases)

    print(f"\nAverage execution time over {len(test_cases)} test cases:")
    print(f"Function 1: {avg_time_func1:.6f} seconds")
    print(f"Function 2: {avg_time_func2:.6f} seconds")
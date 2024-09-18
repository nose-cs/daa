import random
from solution import build_tree
from typing import List

MAX_TREE_SIZE = 200


def run_tests(count=1_000_000):
    print("Starting runing tests...")
    for _ in range(0, count):
        nodes_count = random.randint(1, MAX_TREE_SIZE)
        tree = generate_random_tree(nodes_count)
        x, y = count_distances(tree)
        n, edges = build_tree(x, y)
        if not validate_solution(x, y, n, edges):
            t = build_tree_list(n, edges)
            raise Exception(f"Invalid tree {t}, for x={x} and y={y}. Example: {tree}")
    print("All tests passed...")


def validate_solution(x, y, n, edges):
    tree = build_tree_list(n, edges)
    if not is_tree(tree):
        return False
    even_count, odd_count = count_distances(tree)
    return even_count == x and odd_count == y


def build_tree_list(n: int, edges: List[int]):
    tree = {}
    for i in range(1, n + 1):
        tree[i] = []
    for edge in edges:
        a, b = edge
        tree[a].append(b)
        tree[b].append(a)
    return tree


def count_distances(graph):
    distances = {}

    def dfs(node, parent, depth):
        distances[node] = depth
        for neighbor in graph[node]:
            if neighbor != parent:
                dfs(neighbor, node, depth + 1)

    dfs(1, -1, 0)

    even_count = 0
    odd_count = 0

    for node1, dist1 in distances.items():
        for node2, dist2 in distances.items():
            distance = abs(dist1 - dist2)
            if distance % 2 == 0:
                even_count += 1
            else:
                odd_count += 1

    return even_count, odd_count


def is_tree(graph):
    n = len(graph)
    visited = set()

    def dfs(node, parent):
        visited.add(node)
        for neighbor in graph[node]:
            if neighbor == parent:
                continue
            if neighbor in visited or not dfs(neighbor, node):
                return False
        return True

    if not dfs(1, -1):
        return False

    return len(visited) == n


def generate_random_tree(n):
    if n <= 1:
        return {i + 1: [] for i in range(n)}

    graph = {i + 1: [] for i in range(n)}
    nodes = list(range(1, n + 1))
    random.shuffle(nodes)

    for i in range(1, n):
        u = nodes[i]
        v = nodes[random.randint(0, i - 1)]
        graph[u].append(v)
        graph[v].append(u)

    return graph

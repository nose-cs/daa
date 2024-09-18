import math


def build_tree(x: int, y: int):
    if not x >= y >= 0:
        return None, None

    # Calculate n and s
    sum_xy = x + y
    diff_xy = x - y

    n = int(math.isqrt(sum_xy))
    s = int(math.isqrt(diff_xy))

    # Verify exists sqrt
    if n ** 2 != sum_xy or s ** 2 != diff_xy:
        return None, None

    # Check parity
    if (n % 2) != (s % 2):
        return None, None

    # Calculate a and b
    a = (n + s) // 2
    b = (n - s) // 2

    # Check for non-negative a and b
    if a < 0 or b < 0:
        return None, None

    # Zero vertex is not a tree
    if n <= 0:
        return None, None

    # One vertex tree
    if n == 1:
        return 1, []

    # If n > 1 at least must be one vertex in A and B
    if a == 0 or b == 0:
        return None, None

    edges = []
    current_vertex = 1

    # Build tree for set A, a - 1 vertices from A plus 1 from B
    vertex_b = current_vertex
    current_vertex += 1

    if a > 1:
        for i in range(current_vertex, current_vertex + a - 1):
            edges.append((i, vertex_b))

        current_vertex += a - 1

    # Build tree for set B, b - 1 vertices from B plus 1 from A
    vertex_a = current_vertex
    current_vertex += 1

    if b > 1:
        for i in range(current_vertex, current_vertex + b - 1):
            edges.append((i, vertex_a))

    # Connect the two trees
    edges.append((vertex_a, vertex_b))

    return n, edges

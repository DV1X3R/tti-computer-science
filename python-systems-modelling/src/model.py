#! Discrete Event Simulation Model.

from generators import *
import matplotlib.pyplot as plt


class Model:
    duration = 0.0  # Simulation duration.
    time = 0.0  # Current time.
    queue = []  # Queue, rly?

    proc_end_time = float('inf')  # Request processing end time.
    req1_gen_time = erlang_rand()  # Next generation time for request 1.
    req2_gen_time = poisson_rand()  # Next generation time for request 2.

    server_work_time = 0.0  # Used to calculate server coefficients.
    queue_history = [[], [], []]  # Used to show a beautiful plot at the end.

    # Print table header.
    def __init__(self, t):
        print("%10s \t %10s \t %10s \t %10s \t %10s \t %10s %10s \t %10s"
              % ("Event", "tm", "l1", "l2", "h", "S", "n", "Q"))
        self.print_event("")
        self.duration = t

    # Print a table row.
    def print_event(self, event):
        self.queue_history[0].append(self.time)
        self.queue_history[1].append(self.queue.count(1))
        self.queue_history[2].append(self.queue.count(2))
        print("%10s \t %10.2f \t %10.2f \t %10.2f \t %10.2f \t %10r %10d \t %10s"
              % (event, self.time, self.req1_gen_time, self.req2_gen_time
                 , self.proc_end_time, False if self.proc_end_time == float('inf') else True
                 , len(self.queue), self.queue))

    # Add to the queue or start processing.
    def process(self, req_type):
        # If the server is busy, then add request to the queue.
        # Otherwise lock the server and generate new proc_end_time.
        if self.proc_end_time != float('inf'):
            self.queue.append(req_type)
        else:
            if req_type == 1:
                self.proc_end_time = normal_rand() + self.time
            elif req_type == 2:
                self.proc_end_time = exponential_rand() + self.time
            self.server_work_time += \
                (self.proc_end_time if self.proc_end_time <= self.duration else self.duration) - self.time

    def run(self):
        while self.time <= self.duration:

            # Generate type 1 request.
            if self.time >= self.req1_gen_time:
                self.req1_gen_time = erlang_rand() + self.time
                self.process(1)
                self.print_event("L1 Arrived")

            # Generate type 2 request.
            if self.time >= self.req2_gen_time:
                self.req2_gen_time = poisson_rand() + self.time
                self.process(2)
                self.print_event("L2 Arrived")

            # Finish processing.
            if self.time >= self.proc_end_time:
                self.proc_end_time = float('inf')
                # If queue is not empty, then process the following request.
                if self.queue:
                    # q = self.queue.pop(len(self.queue) - 1)  # LIFO
                    q = self.queue.pop(0)  # FIFO
                    self.process(q)
                self.print_event("Processed")

            # Update simulation time with the nearest event.
            self.time = min(self.req1_gen_time, self.req2_gen_time, self.proc_end_time)

        # Summary Statistics.
        queue_history_sum = list(map(lambda x, y: x + y, self.queue_history[1], self.queue_history[2]))
        server_work_cf = self.server_work_time / self.duration

        print("\nServer work coefficient: %.2f \nServer idle coefficient: %.2f \n"
              "Max queue length: %d \nAvg queue length: %d"
              % (server_work_cf, 1 - server_work_cf
                 , max(queue_history_sum), (sum(queue_history_sum) / len(queue_history_sum))))

        # Build plot.
        plt.plot(self.queue_history[0], self.queue_history[1], color='C0', label='Type 1')
        plt.plot(self.queue_history[0], self.queue_history[2], color='C1', label='Type 2')
        plt.plot(self.queue_history[0], queue_history_sum, color='C9', label='Total')
        plt.title('Queue')
        plt.xlabel('t')
        plt.ylabel('length')
        plt.legend()
        plt.show()


# Start simulation.
Model(t=500).run()

#! Discrete Event Simulation Model.

from generators import *
import matplotlib.pyplot as plt


class Model:
    queue = []
    processing = False  # Server status.
    time = 0.0  # Simulation time.
    proc_end_time = 0.0  # Request processing end time.
    req1_gen_time = erlang_rand()  # Next generation time for request 1.
    req2_gen_time = poisson_rand()  # Next generation time for request 2.

    queue_history = [[], [], []]  # Used to show beautiful plot at the end.
    queue_len_history = []  # Used to calculate average and max queue len.
    server_work_time = 0.0  # Used to calculate coefficients.

    # Print table header on init.
    def __init__(self):
        print("%10s \t %10s \t %10s \t %10s \t %10s \t %10s %10s \t %10s"
              % ("Event", "tm", "l1", "l2", "h", "S", "n", "Q"))
        self.print_event("", 0.0)

    # Print table row with actual information.
    def print_event(self, event, max_table_t):
        # history
        self.queue_len_history.append(len(self.queue))
        self.queue_history[0].append(self.time)
        self.queue_history[1].append(self.queue.count(1))
        self.queue_history[2].append(self.queue.count(2))

        if self.time <= max_table_t:
            print("%10s \t %10.2f \t %10.2f \t %10.2f \t %10.2f \t %10r %10d \t %10s"
                  % (event, self.time, self.req1_gen_time, self.req2_gen_time
                     , self.proc_end_time, self.processing, len(self.queue), self.queue))

    def process_request(self, request_type):
        # Process only requests with type 1 or 2.
        if request_type in (1, 2):
            # If server is busy, then add request to the queue.
            # Otherwise lock the server and generate new proc_end_time.
            if self.processing:
                self.queue.append(request_type)
            else:
                if request_type == 1:
                    self.proc_end_time = normal_rand() + self.time
                elif request_type == 2:
                    self.proc_end_time = exponential_rand() + self.time
                self.processing = True  # Lock server (Further requests will go to the queue).
                self.server_work_time += self.proc_end_time - self.time  # Total server usage time.

    def run(self, t, max_table_t):
        while self.time <= t:

            # Generate type 1 request.
            if self.time >= self.req1_gen_time:
                self.req1_gen_time = erlang_rand() + self.time
                self.process_request(1)  # Add to the queue or start processing.
                self.print_event("L1 Arrived", max_table_t)

            # Generate type 2 request.
            if self.time >= self.req2_gen_time:
                self.req2_gen_time = poisson_rand() + self.time
                self.process_request(2)  # Add to the queue or start processing.
                self.print_event("L2 Arrived", max_table_t)

            # Finish processing.
            if self.time >= self.proc_end_time and self.processing:
                self.processing = False  # Unlock server.
                self.proc_end_time = 0.0

                # If queue is not empty.
                if len(self.queue) != 0:
                    # q = self.queue.pop(len(self.queue) - 1)  # LIFO
                    q = self.queue.pop(0)  # FIFO
                    self.process_request(q)  # Add to the queue or start processing.
                self.print_event("Processed", max_table_t)

            # Update simulation time with the nearest event.
            # If server is busy, then we should take care about proc_end_time too (process finishing time).
            if self.processing:
                self.time = float(min(self.req1_gen_time, self.req2_gen_time, self.proc_end_time))
            else:
                self.time = float(min(self.req1_gen_time, self.req2_gen_time))

        # End of the simulation. Results.
        print("\nServer work coefficient: %.2f \nServer idle coefficient: %.2f \n"
              "Max queue length: %d \nAvg queue length: %d"
              % ((self.server_work_time / t), (1 - self.server_work_time / t)
                 , max(self.queue_len_history), (sum(self.queue_len_history) / len(self.queue_len_history))))

        if t != max_table_t:
            print("\nWarning: Only first " + str(max_table_t) + " records were shown in the table!")

        # Build plot
        plt.plot(self.queue_history[0], self.queue_history[1], color='C0', label='Type 1')
        plt.plot(self.queue_history[0], self.queue_history[2], color='C1', label='Type 2')
        plt.plot(self.queue_history[0], self.queue_len_history, color='C9', label='Total')
        plt.title('Queue Length')
        plt.xlabel('t')
        plt.ylabel('length')
        plt.legend()
        plt.show()


# Start simulation.
Model().run(t=50000, max_table_t=500)

#! Discrete Event Simulation Model.

from generators import *


class Model:
    queue = []
    processing = False  # Server status.
    time = 0.0  # Simulation time.
    proc_end_time = 0.0  # Request processing end time.
    req1_gen_time = erlang_rand()  # Next generation time for request 1.
    req2_gen_time = poisson_rand()  # Next generation time for request 2.

    queue_len_history = []  # Used to calculate average and max queue len.
    server_work_time = 0.0  # Used to calculate coefficients.

    # Print table header on init.
    def __init__(self):
        print("%10s \t %10s \t %10s \t %10s \t %10s \t %10s %10s \t %10s"
              % ("Event", "tm", "l1", "l2", "h", "S", "n", "Q"))
        self.print_event("")

    # Print table row with actual information.
    def print_event(self, event):
        print("%10s \t %10.2f \t %10.2f \t %10.2f \t %10.2f \t %10r %10d \t %10s"
              % (event, self.time, self.req1_gen_time, self.req2_gen_time
                 , self.proc_end_time, self.processing, len(self.queue), self.queue))

    def process_request(self, request_type):
        # Process only requests with type 1 or 2.
        if request_type not in (1, 2):
            return

        # If server is busy, then add request to the queue.
        # Otherwise lock the server and generate new proc_end_time.
        if self.processing:
            self.queue.append(request_type)  # queue.
            self.queue_len_history.append(len(self.queue))  # history.
        else:
            if request_type == 1:
                self.proc_end_time = normal_rand() + self.time
            elif request_type == 2:
                self.proc_end_time = exponential_rand() + self.time

            self.server_work_time += self.proc_end_time - self.time  # Total server usage time.
            self.processing = True  # Lock server (Further requests will go to the queue).

    def run(self, t):
        while self.time <= t:

            # Generate type 1 request.
            if self.time >= self.req1_gen_time:
                self.req1_gen_time = erlang_rand() + self.time
                self.process_request(1)  # Add to the queue or start processing.
                self.print_event("L1 Arrived")

            # Generate type 2 request.
            if self.time >= self.req2_gen_time:
                self.req2_gen_time = poisson_rand() + self.time
                self.process_request(2)  # Add to the queue or start processing.
                self.print_event("L2 Arrived")

            # Finish processing.
            if self.time >= self.proc_end_time and self.processing:
                self.processing = False
                self.proc_end_time = 0.0

                # If queue is not empty.
                if len(self.queue) != 0:
                    # q = self.queue.pop(len(self.queue) - 1)  # LIFO
                    q = self.queue.pop(0)  # FIFO
                    self.process_request(q)  # Add to the queue or start processing.
                self.print_event("Processed")

            # If server is busy, then we should take care about proc_end_time too (process finishing time).
            if self.processing:
                self.time = min(self.req1_gen_time, self.req2_gen_time, self.proc_end_time)
            else:
                self.time = min(self.req1_gen_time, self.req2_gen_time)

        print("\nServer work coefficient: %.2f \nServer idle coefficient: %.2f \n"
              "Max queue length: %d \nAvg queue length: %d"
              % ((self.server_work_time / t), (1 - self.server_work_time / t)
                 , max(self.queue_len_history), (sum(self.queue_len_history) / len(self.queue_len_history))))


# Start simulation.
Model().run(t=500)

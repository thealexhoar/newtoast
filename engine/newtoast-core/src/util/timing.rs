use std::collections::VecDeque;

const NUM_FRAME_TIMES: usize = 64;

pub struct InterpolatingTimer {
    start: std::time::Instant,
    last: std::time::Instant,
    accumulator: f64,
    timestep: f64,
    frame_time_history: VecDeque<f64>,
    averaged_frame_time: f64,
}

impl InterpolatingTimer {
    pub fn new(timestep: f64) -> Self {
        let now = std::time::Instant::now();
        let frame_time_history = VecDeque::from(vec![0.0; NUM_FRAME_TIMES]);
        Self {
            start: now,
            last: now,
            accumulator: 0.0,
            timestep,
            frame_time_history,
            averaged_frame_time: 0.0,
        }
    }

    pub fn reset(&mut self) {
        let now = std::time::Instant::now();
        self.start = now;
        self.last = now;
        self.accumulator = 0.0;
    }

    pub fn elapsed(&self) -> f64 {
        (self.last - self.start).as_secs_f64()
    }

    pub fn latest_frame_rate(&self) -> f64 {
        if let Some(&latest) = self.frame_time_history.back() {
            if latest > 0.0 {
                1.0 / latest
            }
            else { 0.0 }
        }
        else {
            0.0
        }
    }

    pub fn average_frame_rate(&self) -> f64 {
        if self.averaged_frame_time > 0.0 {
            1.0 / self.averaged_frame_time
        }
        else {
            0.0
        }
    }

    pub fn tick(&mut self) -> (u32, f64) {
        let now = std::time::Instant::now();
        let frame_time = (now - self.last).as_secs_f64();
        self.last = now;

        // avoid spiral of death
        let frame_time = if frame_time > 0.25 { 0.25 } else { frame_time };

        self.accumulator += frame_time;

        self.frame_time_history.pop_front();
        self.frame_time_history.push_back(frame_time);
        self.averaged_frame_time = self.frame_time_history.iter().sum::<f64>() / (NUM_FRAME_TIMES as f64);

        let mut updates = 0;
        while self.accumulator >= self.timestep {
            self.accumulator -= self.timestep;
            updates += 1;
        }

        let alpha = self.accumulator / self.timestep;

        (updates, alpha)
    }
}
# Player transitions

State updates return the first eligible next state, or `null` to stay active.
Inherited checks run first; each caller returns an inherited choice immediately.
Only `StateMachine` applies transitions, after the outgoing update returns. This
keeps velocity writes and further transition checks from running after exit.

Exit and entry run once, synchronously within the same Player update. The next
state does not update until the next Player update. Returning the current state
still runs exit and entry: basic attack uses this restart behavior for combos.

Basic attack retains its end-of-frame coroutine. Leaving attack cancels pending
work. The state machine also checks that attack is still current before honoring
a delayed restart.

Priority follows the existing check order:

- Dash before state-specific actions, when dash is eligible.
- Grounded: jump, falling, attack, then idle/move behavior.
- Airborne: wall slide, attack, then jump/fall-specific checks.
- Dash: wall cancellation before timer expiry.
- Wall slide: grounded exit, lost wall contact, then wall jump.

This keeps the existing inheritance and animation assets. Transition requests
are return values so normal control flow ends the outgoing update; no exceptions,
deferred transition queue, or repeated current-state checks are needed.
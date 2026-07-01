# WHAT_I_WOULD_DO_NEXT.md

If I had two additional weeks, this is what I would build next, in priority order.

## Priority 1 — Features that would fundamentally improve the experience

**Per-employee access instead of a hardcoded ID.**
Right now the dashboard shows a single hardcoded employee. The first real improvement
would be a shareable per-employee link (e.g. `/dashboard/{employeeId}`), so HR can send
each new hire their own link. This turns the app from a demo into something usable by
the whole company. Lightweight authentication (or at least a non-guessable token per
employee) would follow naturally from this.

**HR interface for entering onboarding data.**
Currently data is seeded by a script. HR — a single person managing 2-3 hires per month
— needs a simple form to add a new employee, assign their manager and buddy, and
customize their checklist. This is the piece that makes the app self-sufficient without
a developer in the loop.

## Priority 2 — Features that would add significant value

**Checklist templates per department.**
An Engineering hire and a Sales hire need different onboarding tasks. Instead of the
same checklist for everyone, HR could define a template per department that is
automatically applied when a new employee is created.

**Working-day-aware scheduling.**
The current seed generates schedule days sequentially, which can place "Remote day" on a
weekend. A proper scheduler would skip weekends and respect the 3-office / 2-remote
hybrid pattern described in the brief.

**Progress overview for HR.**
A simple view where HR can see, across all current new hires, how far each person has
progressed through their checklist — useful for spotting someone who is stuck or falling
behind.

## Priority 3 — Nice-to-have improvements and why they matter

**Mobile-responsive layout.**
A new employee might open the app on their phone before their first day. The current
layout is desktop-first; making it responsive would widen where and when it is useful.

**Links and resources section.**
Direct links to the tools mentioned in the brief (Slack, Google Meet) and to internal
documents (company handbook, IT setup guide) would reduce the "I don't know where
anything is" friction that the app is meant to solve.

**Search and filter in the team directory.**
With 200 employees (the app currently seeds 17), a search box and department filter in
the directory would become necessary. It matters because the directory's usefulness
scales inversely with how hard it is to find a specific person.

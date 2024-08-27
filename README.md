## Tracing with bpftrace

```bash
sudo bpftrace -p (ps aux | grep 'RuntimeProbes' | grep -v grep | awk '{print $2}') -e 'usdt:::myprobe1 {printf("%s %ld %ld\n
", probe, arg0, arg1);}'
```
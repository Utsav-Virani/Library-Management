﻿select s.en,s.name,s.sem,b.bid,b.bname,h.bti,h.date from student s,Books b,history h where s.en = h.en and b.bid = h.bid;
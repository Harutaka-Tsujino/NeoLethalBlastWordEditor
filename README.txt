リーサルブラストは単語を並べて戦うゲームですが
単語には物理特殊属性があるのでそれをコンボボックスで選択できるようにしました、そのほかにもUXのことを考えidの自動振り分けなど、プランナーが操作する必要のないものは操作できないようにし自動で行うようにしています。
列挙体を取得し自動でコンボボックスを作る等
別のデーターベースを取得してきた場合も動くように頑張りました。

追記2019/07/26
DDLとDMLにsql文を分離し、
DDL自体とそれのみで動くように修正しました。
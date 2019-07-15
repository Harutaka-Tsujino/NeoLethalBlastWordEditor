CREATE `neo_lethal_blast`;
USE `neo_lethal_blast`;
CREATE TABLE `word_datas` (
  `id` INT NOT NULL,
  `name` VARCHAR(45) NOT NULL,
  `type` ENUM('SMASH', 'SLASH', 'PENE') NOT NULL,
  `element` ENUM('FIRE', 'WATER', 'WIND') NOT NULL,
  `not_use` TINYINT NULL,
  PRIMARY KEY (`id`));

リーサルブラストは単語を並べて戦うゲームですが
単語には物理特殊属性があるのでそれをコンボボックスで選択できるようにしました、そのほかにもUXのことを考えidの自動振り分けなど、プランナーが操作する必要のないものは操作できないようにし自動で行うようにしています。
列挙体を取得し自動でコンボボックスを作る等
別のデーターベースを取得してきた場合も動くように頑張りました。
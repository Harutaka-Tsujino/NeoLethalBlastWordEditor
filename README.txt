CREATE `neo_lethal_blast`;
USE `neo_lethal_blast`;
CREATE TABLE `word_datas` (
  `id` INT NOT NULL,
  `name` VARCHAR(45) NOT NULL,
  `type` ENUM('SMASH', 'SLASH', 'PENE') NOT NULL,
  `element` ENUM('FIRE', 'WATER', 'WIND') NOT NULL,
  `not_use` TINYINT NULL,
  PRIMARY KEY (`id`));

���[�T���u���X�g�͒P�����ׂĐ키�Q�[���ł���
�P��ɂ͕������ꑮ��������̂ł�����R���{�{�b�N�X�őI���ł���悤�ɂ��܂����A���̂ق��ɂ�UX�̂��Ƃ��l��id�̎����U�蕪���ȂǁA�v�����i�[�����삷��K�v�̂Ȃ����̂͑���ł��Ȃ��悤�ɂ������ōs���悤�ɂ��Ă��܂��B
�񋓑̂��擾�������ŃR���{�{�b�N�X����铙
�ʂ̃f�[�^�[�x�[�X���擾���Ă����ꍇ�������悤�Ɋ撣��܂����B
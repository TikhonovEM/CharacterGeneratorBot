using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterGeneratorBot.MorkBorg
{
    public static class CharacterToMarkdownConverter
    {
        public static string Convert(Character character)
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(character.Name))
                sb.AppendFormat("*Имя:* {0}{1}{1}", character.Name, Environment.NewLine);

            if (character.Health.HasValue)
                sb.AppendFormat("*Очки здоровья:* {0}{1}{1}", character.Health.Value, Environment.NewLine);

            #region STUB

            var r = new Random();
            var money = (r.Next(1, 6) + r.Next(1, 6)) * 10;
            sb.AppendFormat("*Серебро:* {0}{1}{1}", money, Environment.NewLine);

            var foodCount = r.Next(1, 4);
            sb.AppendFormat("*Запас еды на {0} дня* {1}{1}", foodCount, Environment.NewLine);
            #endregion

            if (character.Description != null && character.Description.Any())
            {
                sb.AppendLine("*Описание персонажа*:");
                for (var i = 0; i < character.Description.Length; i++)
                    sb.AppendFormat("{0}. {1}{2}", i + 1, character.Description[i], Environment.NewLine);
                sb.AppendLine();
            }

            if (character.Skills != null)
            {
                sb.AppendLine("*Навыки персонажа:*");
                if (character.Skills.Skill.HasValue)
                    sb.AppendFormat("*Сноровка*: {0}{1}", character.Skills.Skill.Value, Environment.NewLine);
                if (character.Skills.Control.HasValue)
                    sb.AppendFormat("*Контроль*: {0}{1}", character.Skills.Control.Value, Environment.NewLine);
                if (character.Skills.Strength.HasValue)
                    sb.AppendFormat("*Удаль*: {0}{1}", character.Skills.Strength.Value, Environment.NewLine);
                if (character.Skills.Durability.HasValue)
                    sb.AppendFormat("*Стойкость*: {0}{1}", character.Skills.Durability.Value, Environment.NewLine);
                sb.AppendLine();
            }

            if (!string.IsNullOrEmpty(character.WeaponOne))
                sb.AppendFormat("*Первое оружие:* {0}{1}{1}", character.WeaponOne, Environment.NewLine);

            if (!string.IsNullOrEmpty(character.WeaponTwo))
                sb.AppendFormat("*Второе оружие:* {0}{1}{1}", character.WeaponTwo, Environment.NewLine);

            if (!string.IsNullOrEmpty(character.Armor))
                sb.AppendFormat("*Броня:* {0}{1}{1}", character.Armor, Environment.NewLine);

            if (character.Items != null && character.Items.Any())
            {
                sb.AppendLine("*Снаряжение*:");
                for (var i = 0; i < character.Items.Length; i++)
                    sb.AppendFormat("{0}. {1}{2}", i + 1, character.Items[i], Environment.NewLine);
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
